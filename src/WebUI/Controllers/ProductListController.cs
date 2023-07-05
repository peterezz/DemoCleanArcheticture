using Clean_architecture.Application.ProductList.Commands.CreateDocument;
using Clean_architecture.Application.ProductList.Queries.GetProductList;
using Clean_architecture.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;
public class ProductListController : ApiControllerBase
{
    [HttpGet("index")]
    public async Task<IActionResult> Index()
    {
        var data = await Mediator.Send(new GetProductListQuery());
        return Ok(data);

    }
    [HttpGet("boolQuery")]
    public async Task<IActionResult> ExceBoolQueryAsync(GetSpacficProductListQuery getSpacficProductList)
    {
        var data = await Mediator.Send(getSpacficProductList);
        return Ok(data);
    }
    [HttpGet("sumAggQuery")]
    public async Task<IActionResult> ExceSumQueryAsync(GetSumProductListQuery getSumProductListQuery)
    {
        var data = await Mediator.Send(getSumProductListQuery);
        return Ok(data);
    }
    [HttpGet("BucketQuery")]
    public async Task<IActionResult> ExceFilterQuery(GetFilterProductListQuery getFilterProductListQuery)
    {
        var data = await Mediator.Send(getFilterProductListQuery);
        return Ok(data);
    }
    [HttpPost]
    public async Task<IActionResult> CreateCommand(CreateDocumentCommand createDocumentCommand)
    {
        var data = await Mediator.Send(createDocumentCommand);
        return Ok(data);
    }



}
