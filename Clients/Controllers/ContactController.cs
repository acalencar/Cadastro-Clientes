

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


[Route("contact")]

public class ContactController : ControllerBase
{



    [Route("")]
    [HttpGet]
    public async Task<ActionResult<List<Contact>>> GetContact([FromServices] DataContext context)
    {
        var contacts = await context.Contacts.AsNoTracking().ToListAsync();

        return Ok(contacts);

    }

    [Route("{id:int}")]
    [HttpGet]
    public async Task<ActionResult<List<Contact>>> GetByContactId(
        int id,
        [FromBody] DataContext context
        )
    {
        var contacts = await context.Contacts.FirstOrDefaultAsync(x => x.Id == id);

        return Ok(contacts);
    }

    [Route("")]
    [HttpPost]
    public async Task<ActionResult<List<Contact>>> PostContact(
        [FromBody] Contact ContactModel,
        [FromServices] DataContext context
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { message = "Contato inválido!" });
        }
        try
        {
            context.Contacts.Add(ContactModel);
            await context.SaveChangesAsync();
        }
        catch { return BadRequest(new { message = "Não foi possível salvar o contato" }); }

        return Ok(context.Contacts);

    }

    [Route("{id:int}")]
    [HttpPut]
    public async Task<ActionResult<List<Contact>>> PutContact(
    int id,
    [FromBody] Contact ContactModel,
    [FromServices] DataContext context)
    {
        if (id != ContactModel.Id)
            return BadRequest(new { message = "Cliente não Encontrado" });

        if (!ModelState.IsValid)
            return BadRequest(new { message = "Contact inválido!" });

        try
        {
            //atualizando entidade
            context.Entry<Contact>(ContactModel).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return Ok(ContactModel);
        }

        catch (DbUpdateConcurrencyException)
        {
            return BadRequest(new { message = "Contact já foi atualizado!" });

        }

    }

    [Route("{id:int}")]
    [HttpDelete]
    public async Task<ActionResult<List<Contact>>> DeleteContact(
        int id,
        [FromServices] DataContext context)
    {
        var contact = await context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
        if (contact == null) { return BadRequest(new { message = "Contato não localizado!" }); }

        try
        {
            context.Remove(contact);
            await context.SaveChangesAsync();
            return Ok(new { message = "Contato removido" });
        }
        catch { BadRequest(new { message = "Falha remover contacto" }); }

        return Ok();
    }


}