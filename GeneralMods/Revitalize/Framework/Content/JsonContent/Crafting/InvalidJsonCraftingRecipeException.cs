using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omegasis.Revitalize.Framework.Crafting.JsonContent
{
    public class InvalidJsonCraftingRecipeException:Exception
    {
        public InvalidJsonCraftingRecipeException()
        {

        }

        public InvalidJsonCraftingRecipeException(string Message):base(Message)
        {

        }
    }
}