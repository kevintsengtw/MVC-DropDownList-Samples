using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Demo.Infrastructure.Helpers
{
    public class WebSiteHelper
    {
        /// <summary>
        /// 產生下拉選單html(以IDictionary傳入下拉選單的值).
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="optionData">下拉選單Option的Text與Value.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <param name="defaultSelectValue">預選值.</param>
        /// <param name="appendOptionLabel">是否加入預設空白選項.</param>
        /// <param name="optionLabel">如果appendOptionLabel為true,optionLabel為第一個項目要顯示的文字,如果沒有指定則顯示[請選擇].</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">name;產生DropDownList時 tag Name 不得為空</exception>
        public static MvcHtmlString GetDropdownList(
            string name,
            IDictionary<string, string> optionData,
            object htmlAttributes = null,
            string defaultSelectValue = "",
            bool appendOptionLabel = false,
            string optionLabel = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name", "產生DropDownList時 tag Name 不得為空");
            }

            var select = new TagBuilder("select");
            select.Attributes.Add("name", name);
            var renderHtmlTag = new StringBuilder();

            IDictionary<string, string> newOptionData = new Dictionary<string, string>();

            if (appendOptionLabel)
            {
                newOptionData.Add(new KeyValuePair<string, string>(
                    "",
                    string.IsNullOrWhiteSpace(optionLabel) ? "請選擇" : optionLabel));
            }

            foreach (var item in optionData)
            {
                newOptionData.Add(item);
            }

            foreach (var option in newOptionData)
            {
                var optionTag = new TagBuilder("option");
                optionTag.Attributes.Add("value", option.Key);

                if (!string.IsNullOrEmpty(defaultSelectValue)
                    &&
                    defaultSelectValue.Equals(option.Key))
                {
                    optionTag.Attributes.Add("selected", "selected");
                }

                optionTag.SetInnerText(option.Value);
                renderHtmlTag.AppendLine(optionTag.ToString(TagRenderMode.Normal));
            }
            select.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            select.InnerHtml = renderHtmlTag.ToString();
            return new MvcHtmlString(select.ToString());
        }

    }
}