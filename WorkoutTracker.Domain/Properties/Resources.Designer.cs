﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WorkoutTracker.Domain.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WorkoutTracker.Domain.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to smtp.gmail.com.
        /// </summary>
        public static string EmailHost {
            get {
                return ResourceManager.GetString("EmailHost", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to rlskdwrrrlpmoqyg.
        /// </summary>
        public static string EmailPassword {
            get {
                return ResourceManager.GetString("EmailPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to workout.masters.info@gmail.com.
        /// </summary>
        public static string EmailUsername {
            get {
                return ResourceManager.GetString("EmailUsername", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!DOCTYPE html&gt;
        ///&lt;html&gt;
        ///	&lt;head&gt;
        ///		&lt;title&gt;&lt;/title&gt;
        ///		&lt;meta http-equiv=&quot;Content-Type&quot; content=&quot;text/html; charset=utf-8&quot; /&gt;
        ///		&lt;meta name=&quot;viewport&quot; content=&quot;width=device-width, initial-scale=1&quot; /&gt;
        ///		&lt;meta http-equiv=&quot;X-UA-Compatible&quot; content=&quot;IE=edge&quot; /&gt;
        ///		&lt;style type=&quot;text/css&quot;&gt;
        ///			@media screen {
        ///				@font-face {
        ///					font-family: &apos;Lato&apos;;
        ///					font-style: normal;
        ///					font-weight: 400;
        ///					src: local(&apos;Lato Regular&apos;), local(&apos;Lato-Regular&apos;),
        ///						url(https://fonts.gstatic.com/s/lato/v11/qIIYRU- [rest of string was truncated]&quot;;.
        /// </summary>
        public static string ForgotPasswordEmailTemplate {
            get {
                return ResourceManager.GetString("ForgotPasswordEmailTemplate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;!DOCTYPE html&gt;
        ///&lt;html&gt;
        ///	&lt;head&gt;
        ///		&lt;title&gt;&lt;/title&gt;
        ///		&lt;meta http-equiv=&quot;Content-Type&quot; content=&quot;text/html; charset=utf-8&quot; /&gt;
        ///		&lt;meta name=&quot;viewport&quot; content=&quot;width=device-width, initial-scale=1&quot; /&gt;
        ///		&lt;meta http-equiv=&quot;X-UA-Compatible&quot; content=&quot;IE=edge&quot; /&gt;
        ///		&lt;style type=&quot;text/css&quot;&gt;
        ///			@media screen {
        ///				@font-face {
        ///					font-family: &apos;Lato&apos;;
        ///					font-style: normal;
        ///					font-weight: 400;
        ///					src: local(&apos;Lato Regular&apos;), local(&apos;Lato-Regular&apos;),
        ///						url(https://fonts.gstatic.com/s/lato/v11/qIIYRU- [rest of string was truncated]&quot;;.
        /// </summary>
        public static string VerifyAccountEmailTemplate {
            get {
                return ResourceManager.GetString("VerifyAccountEmailTemplate", resourceCulture);
            }
        }
    }
}
