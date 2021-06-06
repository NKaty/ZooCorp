﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooCorp.BusinessLogic.Animals;
using ZooCorp.BusinessLogic.Employees.HireValidators.ValidationErrors;

namespace ZooCorp.BusinessLogic.Employees.HireValidators
{
    public abstract class HireValidator
    {
        public abstract List<ValidationError> ValidateEmployee(IEmployee employee, List<Animal> animals);
    }
}