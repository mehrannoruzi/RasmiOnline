﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RasmiOnline.Domain.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ErrorMessage {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessage() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("RasmiOnline.Domain.Properties.ErrorMessage", typeof(ErrorMessage).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to فرمت شماره کارت صحیح نمیباشد، لطفا مانند ####-####-####-#### وارد نمایید.
        /// </summary>
        public static string CardNumberFormat {
            get {
                return ResourceManager.GetString("CardNumberFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to  لطفا ایمیل خودرا به صورت صحیح وارد نمایید.
        /// </summary>
        public static string Email {
            get {
                return ResourceManager.GetString("Email", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ایمیل وارد شده صحیح نمیباشد.
        /// </summary>
        public static string InvalidEmailAddress {
            get {
                return ResourceManager.GetString("InvalidEmailAddress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to شماره مشترک نا معتبر می باشد.
        /// </summary>
        public static string InvalidMobileNumber {
            get {
                return ResourceManager.GetString("InvalidMobileNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to قالب ورودی باید آدرس استاندارد اینترنتی باشد.
        /// </summary>
        public static string InvalidUrl {
            get {
                return ResourceManager.GetString("InvalidUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to تنها عدد مجاز است.
        /// </summary>
        public static string JustNumber {
            get {
                return ResourceManager.GetString("JustNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to تعداد کاراکترها باید بین {2} تا {1} باشد.
        /// </summary>
        public static string LengthRange {
            get {
                return ResourceManager.GetString("LengthRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to لطفا بیشتر از {1} کاراکتر وارد ننمایید.
        /// </summary>
        public static string MaxLength {
            get {
                return ResourceManager.GetString("MaxLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to حداقل کاراکتر مجاز 5  و حداکثر {1} می باشد..
        /// </summary>
        public static string Min5MaxLength {
            get {
                return ResourceManager.GetString("Min5MaxLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to حداقل کاراکتر مجاز 6  و حداکثر {1} می باشد..
        /// </summary>
        public static string Min6MaxLength {
            get {
                return ResourceManager.GetString("Min6MaxLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to لطفا کمتر از {1} کاراکتر وارد ننمایید.
        /// </summary>
        public static string MinLength {
            get {
                return ResourceManager.GetString("MinLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to لطفا شماره همراه را صحیح وارد نمایید.
        /// </summary>
        public static string MobileError {
            get {
                return ResourceManager.GetString("MobileError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to رمز عبور باید ترکیبی از حروف بزرگ، کوچک و عدد باشد.
        /// </summary>
        public static string PasswordComplexity {
            get {
                return ResourceManager.GetString("PasswordComplexity", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to رمز ها همخوانی ندارند.
        /// </summary>
        public static string PasswordsNotMatched {
            get {
                return ResourceManager.GetString("PasswordsNotMatched", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to لطفا تاریخ را به صورت YYYY/MM/DD  وارد نمایید.
        /// </summary>
        public static string PersianDate {
            get {
                return ResourceManager.GetString("PersianDate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to این فیلد اجباری می باشد.
        /// </summary>
        public static string Required {
            get {
                return ResourceManager.GetString("Required", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to لطفا زمان با به صورت HH:MM  وارد نمایید.
        /// </summary>
        public static string Time {
            get {
                return ResourceManager.GetString("Time", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to کاربر مورد نظر یافت نشد.
        /// </summary>
        public static string UserNotFound {
            get {
                return ResourceManager.GetString("UserNotFound", resourceCulture);
            }
        }
    }
}
