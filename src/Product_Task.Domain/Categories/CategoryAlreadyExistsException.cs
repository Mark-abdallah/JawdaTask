using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Product_Task.Categories
{
    public class CategoryAlreadyExistsException : BusinessException
    {
        public CategoryAlreadyExistsException(string name)
            : base(Product_TaskDomainErrorCodes.CategoryAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
