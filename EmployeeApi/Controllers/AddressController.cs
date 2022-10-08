using Infrastructuur.Dtos;
using Infrastructuur.Entities;
using Infrastructuur.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    [ApiController]
    [Route("api/")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }
        [HttpGet]
        [Route("Addresses")]
        [ProducesResponseType(200, Type = typeof(AddressDto))] // ok
        [ProducesResponseType(400)] // bad request
        [ProducesResponseType(404)] // not found
        public async Task<IActionResult> GetAllAddressesAsync()
        {
            //todo check if valid modelstate and bad errors
            return Ok(await _addressService.GetAllAddressesAsync());
        }

        [HttpGet("Address/{id}")]

        [ProducesResponseType(200, Type = typeof(AddressDto))] // ok
        [ProducesResponseType(400)] // bad request
        [ProducesResponseType(404)] // not found
        public async Task<IActionResult> GetAddresseByIdAsync(int id)
        {
            //todo check if valid modelstate and bad errors
            return Ok(await _addressService.GetAddresseByIdAsync(id));
        }
        // create a new address
        [HttpPost("Address/Create/")]
        [ProducesResponseType(200)] // ok
        [ProducesResponseType(400)] // bad request
        [ProducesResponseType(404)] // not found
        public async Task<IActionResult> CreateAddress(AddressEntity address)
        {
            //todo check if valid modelstate and bad errors
            return Ok(await _addressService.CreateAddressAsync(address));
        }

        // create a new address
        [HttpPost("Address/Update/{id}")]
        [ProducesResponseType(200)] // ok
        [ProducesResponseType(400)] // bad request
        [ProducesResponseType(404)] // not found
        public async Task<IActionResult> UpdateAddress(AddressEntity address, int id)
        {
            //todo check if valid modelstate and bad errors
            var addressToUpdate = await _addressService.GetAddresseByIdAsync(id) ?? null;
            if(addressToUpdate == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("","address is invalid!");
                return BadRequest(ModelState);
            }
            addressToUpdate.AddressLine = address.AddressLine;
            return Ok(await _addressService.UpdateAddressAsync(addressToUpdate));
        }

        // create a new address
        [HttpPost("Address/Delete/{id}")]
        [ProducesResponseType(200)] // ok
        [ProducesResponseType(400)] // bad request
        [ProducesResponseType(404)] // not found
        public async Task<IActionResult> DeleteAddress(int id)
        {
            //todo check if valid modelstate and bad errors
            var addressToDelete = await _addressService.GetAddresseByIdAsync(id) ?? null;
            if (addressToDelete == null)
            {
                return NotFound();
            }
            return Ok(await _addressService.DeleteAddressAsync(addressToDelete));
        }
    }
}
