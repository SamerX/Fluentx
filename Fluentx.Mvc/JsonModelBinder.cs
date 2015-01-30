using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Fluentx.Mvc
{
    /// <summary>
    /// Used for MVC controller view model parameters where you want to serialize JSON posted data (object) to MVC controller.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class JsonBinderAttribute : CustomModelBinderAttribute
    {
        public override IModelBinder GetBinder()
        {
            return new JsonModelBinder();
        }
    }
    public class JsonModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            try
            {
                var strJson = controllerContext.HttpContext.Request.Form[bindingContext.ModelName];
                if (string.IsNullOrEmpty(strJson))
                {
                    return null;
                }
                else
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    var model = serializer.Deserialize(strJson, bindingContext.ModelType);
                    var modelMetaData = ModelMetadataProviders.Current.GetMetadataForType(() => model, bindingContext.ModelType);

                    var validator = ModelValidator.GetModelValidator(modelMetaData, controllerContext);
                    var validationResult = validator.Validate(null);

                    foreach (var item in validationResult)
                    {
                        bindingContext.ModelState.AddModelError(item.MemberName, item.Message);
                    }
                    return model;
                }
            }
            catch (Exception ex)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelType.Name, ex.Message);
                return null;
            }
        }
    }
}
