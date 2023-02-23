using Microsoft.AspNetCore.Mvc.Rendering;
using HtmlTags;
using Net.Pf.Infrastructure.Extensions.Enums;

namespace Net.Pf.Infrastructure.Extensions;


public static class HtmlHelperExtensions
{
    public static HtmlTag Fluent<T>(this IHtmlHelper<T> helper, string tagName) => new HtmlTag(tagName);




    public static HtmlTag Link<T>(this IHtmlHelper<T> helper,
        string text,
        string Url,
        LinkTarget target = LinkTarget._self,
        string rel = "noopener noreferrer"
    )
    {
        var tag = new HtmlTag("a");
        tag.Attr("href", Url);
        tag.Attr("rel", rel);
        tag.Attr("target", target.ToString());
        tag.AddClass("text-decoration-none");
        tag.AddClass("m-1 p-1");

        tag.Text(text);
        return tag;
    }

    public static HtmlTag Properties<T>(this IHtmlHelper<T> helper, object o) where T : class
    {
        if (o == default) return new HtmlTag("div");

        var divTag = new HtmlTag("div");
        divTag.AddClass("d-flex");
        divTag.AddClass("align-items-center");
        divTag.AddClass("justify-content-center");

        foreach (var prop in o.Properties())
        {
            divTag.Append(new HtmlTag("br").Text($"{prop.Key} : {prop.Value}"));
        }

        return divTag;
    }


    /*
    <table class="table table-bordered table-striped table-hover">
        <thead class="table-dark">
            <tr>
                <th scope="col">#</th>
                @foreach (var prop in props)
                {
                    <th scope="col">@prop?.Name </th>
                }
            </tr>
        </thead>
        <tbody>
            @for (int i = 0; i < @Model.Result.Count; i++)
            {
                <tr>
                    @{
                            var o = @Model.Result[i];
                            <th scope="row">@i</th>
                            @foreach (var prop in props)
                            {
                                @try
                                {
                                    <td>@prop?.GetValue(o)?.ToString() </td>
                                }
                                catch (Exception ex)
                                {
                                    <td>@ex.Message</td>
                                }
                            }
                    }
                </tr>
            }
        </tbody>
    </table>


*/
    public static HtmlTag Table<T, TCollectionObject>
        (this IHtmlHelper<T> helper, IEnumerable<TCollectionObject> collection)
        where TCollectionObject : class
    {
        var props = collection.Type().GetProperties();

        var table = new HtmlTag("table").AddClass("table table-bordered table-striped table-hover");
        var thead = new HtmlTag("thead").AddClass("table-dark");
        table.Append(thead);

        var theadTr = new HtmlTag("tr");
        thead.Append(theadTr);
        theadTr.Append(new HtmlTag("th").Attr("scope", "col").Text("#"));
        foreach (var prop in props)
        {
            theadTr.Append(new HtmlTag("th").Attr("scope", "col").Text($" {prop?.Name} "));
        }

        var Arr = collection.ToArray();
        var tbody = new HtmlTag("tbody");
        table.Append(tbody);
        for (int i = 0; i < Arr.Length; i++)
        {
            var tbodyTr = new HtmlTag("tr");
            tbody.Append(tbodyTr);
            tbodyTr.Append(new HtmlTag("th").Attr("scope", "row").Text($"{i}"));
            var o = Arr[i];

            foreach (var prop in props)
            {
                try
                {
                    tbodyTr.Append(new HtmlTag("td").Text($"{prop?.GetValue(o)?.ToString()}"));
                }
                catch (Exception ex)
                {
                    tbodyTr.Append(new HtmlTag("td").Text($"{ex.Message}"));
                }
            }
        }

        return table;
    }









    public static HtmlTag FormBlock<T, TMember>(this IHtmlHelper<T> helper,
        Expression<Func<T, TMember>> expression,
        Action<HtmlTag> labelModifier = null,
        Action<HtmlTag> inputModifier = null
    ) where T : class
    {
        labelModifier ??= _ => { };
        inputModifier ??= _ => { };

        var divTag = new HtmlTag("div");
        divTag.AddClass("form-group");

        var labelTag = helper.Label(expression);
        labelModifier(labelTag);

        var inputTag = helper.Input(expression);
        inputModifier(inputTag);

        divTag.Append(labelTag);
        divTag.Append(inputTag);

        return divTag;
    }
    public static HtmlTag FormBlock1<T, TMember>(this IHtmlHelper<T> helper, Expression<Func<T, TMember>> expression) where T : class
    {
        var divTag = new HtmlTag("div");
        divTag.AddClass("form-group");

        var labelTag = helper.Label(expression);
        var inputTag = helper.Input(expression);

        divTag.Append(labelTag);
        divTag.Append(inputTag);

        return divTag;
    }
    public static HtmlTag FormEmail<T, TMember>
    (
        this IHtmlHelper<T> helper,
        Expression<Func<T, TMember>> expression
    )
    where T : class
    {
        var divTag = new HtmlTag("div");
        divTag.AddClass("form-group");

        var labelTag = helper.Label(expression);
        divTag.Append(labelTag);

        var inputTag = helper.Input(expression);
        inputTag.AddClass("form-control");
        inputTag.Attr("type", "email");
        inputTag.Attr("placeholder", "Enter email");
        divTag.Append(inputTag);

        //var span = helper.Tag(expression, "span");
        //divTag.Append(span);

        /*
			var span = divTag.Add("span");
			span.Attr("asp-validation-for", expression);


			span.Attr("asp-validation-for", expression.ValueOrDefault(x => x.GetMember()));
			divTag.Add("span").Attr("asp-validation-for", expression.Body);
			divTag.Add("span").Attr("asp-validation-for", expression.Body.Reduce());
			divTag.Add("span").Attr("asp-validation-for", expression.Body.Type);
			divTag.Add("span").Attr("asp-validation-for", expression.Parameters.Skip(1).FirstOrDefault());


			//HtmlTags.HtmlHelperExtensions.GetGenerator
			*/



        return divTag;
    }

    /*
		<div class="form-group">
			<label for="Data.EmailAddress">Email address</label>
			<input type="email" class="form-control"
				   name="Data.EmailAddress" id="Data.EmailAddress"
				   placeholder="Enter email" value=@Model?.Data?.EmailAddress>
			<span asp-validation-for="Data.EmailAddress" class="text-danger"></span>
		</div>

		*/

}
