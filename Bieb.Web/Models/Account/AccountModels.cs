using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bieb.Web.Models.Account
{
    public class UserProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "CurrentPassword", ResourceType = typeof(BiebResources.AccountStrings))]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceName = "NewPasswordLengthErrorMessage", MinimumLength = 6, ErrorMessageResourceType = typeof(BiebResources.AccountStrings))]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType = typeof(BiebResources.AccountStrings))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmNewPassword", ResourceType = typeof(BiebResources.AccountStrings))]
        [Compare("NewPassword", ErrorMessageResourceName = "PasswordConfirmErrorMessage", ErrorMessageResourceType = typeof(BiebResources.AccountStrings))]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Prompt = "UserNamePlaceholder", ResourceType = typeof(BiebResources.AccountStrings))]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Prompt = "PasswordPlaceholder", ResourceType = typeof(BiebResources.AccountStrings))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(BiebResources.AccountStrings))]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Prompt = "UserNamePlaceholder", ResourceType = typeof(BiebResources.AccountStrings))]
        public string NewUserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceName = "NewPasswordLengthErrorMessage", MinimumLength = 6, ErrorMessageResourceType = typeof(BiebResources.AccountStrings))]
        [DataType(DataType.Password)]
        [Display(Prompt = "PasswordPlaceholder", ResourceType = typeof(BiebResources.AccountStrings))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Prompt = "ConfirmPasswordPlaceholder", ResourceType = typeof(BiebResources.AccountStrings))]
        [Compare("NewPassword", ErrorMessageResourceName = "PasswordConfirmErrorMessage", ErrorMessageResourceType = typeof(BiebResources.AccountStrings))]
        public string ConfirmNewPassword { get; set; }
    }
}
