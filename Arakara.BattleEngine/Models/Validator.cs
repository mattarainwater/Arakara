﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenswee.Common.Notifications;

namespace Arakara.BattleEngine.Models
{
    public class Validator
    {
        public bool IsValid { get; private set; }

        public Validator()
        {
            IsValid = true;
        }

        public void Invalidate()
        {
            IsValid = false;
        }
    }

    public static class ValidatorExtensions
    {
        public static bool Validate(this object target)
        {
            var validator = new Validator();
            var notificationName = Global.ValidateNotification(target.GetType());
            target.PostNotification(notificationName, validator);
            return validator.IsValid;
        }
    }
}
