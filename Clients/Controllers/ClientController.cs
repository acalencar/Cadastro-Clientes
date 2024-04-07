
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClientController
{


    [Route("clients")]

    public class ClientController : ControllerBase
    {

        [Route("")]
        [HttpGet]


        public async Task<ActionResult<List<Client>>> Get([FromServices] DataContext context)

        {
            var clients = await context.Clients.AsNoTracking().ToListAsync();


            return clients;

        }


        [Route("{id:int}")]
        [HttpGet]

        public async Task<ActionResult<Client>> GetbyIdClients(
            int id,
            [FromServices] DataContext context)
        {
            var clients = await context.Clients.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            return Ok(clients);
        }


        [Route("")]
        [HttpPost]

        public async Task<ActionResult<List<Client>>> PostClients(
            [FromBody] Client ClientModel,
            [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                context.Clients.Add(ClientModel);
                await context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível salvar a alteração" });
            };
            return Ok(ClientModel);

        }


        [Route("{id:int}")]
        [HttpPut]
        public async Task<ActionResult<List<Client>>> PutClient(
            int id,
             [FromBody] Client ClientModel,
             [FromServices] DataContext context
             )
        {
            //Verifica se o id do cliente existe
            if (id != ClientModel.Id)
                return NotFound(new { message = "Cliente não encontrado!" });

            //Verifica se o Modelo enviado é válido
            if (!ModelState.IsValid)
                return BadRequest(ClientModel);


            try
            {
                //atualizando entidade
                context.Entry<Client>(ClientModel).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return Ok(ClientModel);
            }

            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Cliente já foi atualizado!" });

            }


        }

        [Route("{id:int}")]
        [HttpDelete]

        public async Task<ActionResult<List<Client>>> DeleteClients(
            int id,
            [FromServices] DataContext context)
        {

            var client = await context.Clients.FirstOrDefaultAsync(x => x.Id == id);
            if (client == null)
                return NotFound(new { message = "Cliente não encontrado" });

            try
            {
                context.Clients.Remove(client);
                await context.SaveChangesAsync();
                return Ok(new { message = "Cliente removido com sucesso!" });
            }
            catch (Exception)
            {

                return BadRequest(new { message = "Não foi possível remover o Cliente" });

            }
            return Ok();

        }

    }
}
