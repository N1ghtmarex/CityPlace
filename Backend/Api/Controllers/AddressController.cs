using Application.Addresses.Dtos;
using Application.Addresses.Queries;
using Core.EntityFramework.Features.SearchPagination.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/address")]
    public class AddressController(ISender sender) : ControllerBase
    {
        /// <summary>
        /// Получение списка адресов
        /// </summary>
        /// <param name="query">Модель запроса</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResult<AddressListViewModel>> GetAddressList([FromQuery] GetAddressesListQuery query, CancellationToken cancellationToken)
        {
            return await sender.Send(query, cancellationToken);
        }
    }
}
