using System.Security.Claims;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class UserdataController:BaseApiController
{   
    IUserRepository _userRepository;
    IMapper _mapper;
    public UserdataController(IUserRepository userRepository,IMapper mapper)
    {
        _userRepository=userRepository;
        _mapper=mapper;
    }


    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<specialuserDto?>>> getUsers()
    {
        //Console.WriteLine(ids);
        // if(_context==null)
        // yield return null; 
        // await foreach(AppUser i in _context.Users)
        // {
        //     yield return i;
        // }
        var Appmembers=await _userRepository.GetMembersAsync();
        //var members=_mapper.Map<IEnumerable<specialuserDto>>(Appmembers);
        return Ok(Appmembers);
    }
    [HttpGet("{username}")]
    public async Task<ActionResult<specialuserDto?>> getUser(string username)
    {
        //Console.WriteLine(ids);

        var Appmember=await _userRepository.GetMemberByUsernameAsync(username);
        //var member=_mapper.Map<specialuserDto>(Appmember);
        return Ok(Appmember);
        
    }

    [Authorize]
    [HttpGet("down/{id}")]
    public async Task<ActionResult<specialuserDto?>> getUsers(int id)
    {
        //Console.WriteLine(ids);

        var Appmember=await _userRepository.GetUsersByIdAsync(id);
        var member=_mapper.Map<specialuserDto>(Appmember);
        return Ok(member);
        
    }

    [HttpPut]
    public async Task<ActionResult> updateUser(memberUpdateDto updatedcontent)
    {
        var username=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        AppUser user = await _userRepository.GetUserByUsernameAsync(username);

        if(user==null)
        return BadRequest("User Doesn't exists!!");
        _mapper.Map(updatedcontent,user);
        // user.Interests="Some changes";
        if(await _userRepository.SaveAllAsync()==true)
        return NoContent();
        
        return BadRequest("No changes Persisted!!");

    }
}
