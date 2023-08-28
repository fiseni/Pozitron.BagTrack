using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace PozitronDev.BagTrack.Setup;

public class ResponseTypeModelProvider : IApplicationModelProvider
{
    public int Order => 10;

    public void OnProvidersExecuted(ApplicationModelProviderContext context)
    {
        // Intentionally left empty
    }

    public void OnProvidersExecuting(ApplicationModelProviderContext context)
    {
        foreach (var controller in context.Result.Controllers)
        {
            foreach (var action in controller.Actions)
            {
                AddFiltersToAction(action);
            }
        }
    }

    private static void AddFiltersToAction(ActionModel action)
    {
        var verb = action.Attributes.OfType<HttpMethodAttribute>().SelectMany(x => x.HttpMethods).Distinct().FirstOrDefault();

        if (verb is null) return;

        var produceResponseAttributes = action.Attributes.OfType<ProducesResponseTypeAttribute>().ToList();

        if (!produceResponseAttributes.Any(x => x.StatusCode == StatusCodes.Status200OK))
        {
            var responseType = GetResponseType(action.ActionMethod.ReturnType);

            if (responseType is null)
            {
                action.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status200OK));
            }
            else
            {
                action.Filters.Add(new ProducesResponseTypeAttribute(responseType, StatusCodes.Status200OK));
            }
        }

        if (!produceResponseAttributes.Any(x => x.StatusCode == StatusCodes.Status400BadRequest))
        {
            action.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
        }

        // Add NotFound (404) response only if the action is not POST.
        if (!produceResponseAttributes.Any(x => x.StatusCode == StatusCodes.Status404NotFound) && !verb.Equals(HttpMethods.Post))
        {
            action.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status404NotFound));
        }
    }

    private static Type? GetResponseType(Type returnType)
    {
        return returnType switch
        {
            var type when type.IsGenericType &&
                        type.GetGenericTypeDefinition() == typeof(Task<>) &&
                        type.GenericTypeArguments[0].IsGenericType &&
                        type.GenericTypeArguments[0].GetGenericTypeDefinition() == typeof(ActionResult<>) => type.GenericTypeArguments[0].GenericTypeArguments[0],
            var type when type == typeof(Task<ActionResult>) => null,
            var type when type == typeof(Task<IActionResult>) => null,
            var type when type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ActionResult<>) => type.GenericTypeArguments[0],
            var type when type == typeof(ActionResult) => null,
            var type when type == typeof(IActionResult) => null,
            var type when type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Task<>) => type.GenericTypeArguments[0],
            _ => returnType
        };
    }
}
