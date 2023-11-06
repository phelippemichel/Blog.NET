using Blog.Data;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    public class CategoryControllers : ControllerBase
    {
        [HttpGet("v1/categories")]
        public async Task<IActionResult> GetAsync(
            [FromServices]BlogDataContext context)
            {
                var categories = await context.Categories.ToListAsync();
                return Ok(categories);
            }

        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute]int id,
            [FromServices]BlogDataContext context)
            {
                try{
                var category = await context
                .Categories
                .FirstOrDefaultAsync(x => x.Id == id);

                if(category == null)
                return NotFound();

                return Ok(category);
                }catch(Exception ex){
                    return StatusCode(500, "Falha interna");
                }
            }

        [HttpPost("v1/categories/")]
        public async Task<IActionResult> PostAsync(
            [FromBody] EditorCategoryViewModel model,
            [FromServices] BlogDataContext context)
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                try{
                var category = new Category{
                    Id = 0,
                    Name = model.Name,
                    Slug = model.Slug.ToLower(),
                };
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return Created($"v1/categories/{category.Id}", category);
                }catch(DbUpdateException){
                    return StatusCode(500, "01EXC1 - Não foi possível incluir a categoria");
                }catch(Exception ex){
                    return StatusCode(500, "Falha interna");
                }
                
            }

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] EditorCategoryViewModel model,
            [FromServices]BlogDataContext context)
            {

                try{
                var category = await context
                .Categories
                .FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                return NotFound();

                category.Name = model.Name;
                category.Slug = model.Slug;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return Ok(model);
                }catch(DbUpdateException){
                    return StatusCode(500, "01EXC2 - Não foi possível alterar a categoria");
                }catch(Exception ex){
                    return StatusCode(500, "Falha interna");
                }
            }

        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
            [FromServices]BlogDataContext context)
            {
                try{
                var category = await context
                .Categories
                .FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                return NotFound();

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return Ok(category);
                }catch(DbUpdateException){
                    return StatusCode(500, "01EXC3 - Não foi possível excluir a categoria");
                }catch(Exception ex){
                    return StatusCode(500, "Falha interna - teste");
                }
            }
    }
}
