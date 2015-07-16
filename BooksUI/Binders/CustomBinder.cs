using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksUI.Binders
{
    public class CustomBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext,
                                ModelBindingContext bindingContext)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;

            string title = request.Form.Get("Title");
            string author = request.Form.Get("Author");

            if (!string.IsNullOrEmpty(title))
            {
                title = title.Trim();
            }

            if (!string.IsNullOrEmpty(author))
            {
                author = author.Trim();
            }

            return new SearchViewModel
            {
                Title = title,
                Author = author
            };
        }
    } 
}