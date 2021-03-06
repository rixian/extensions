//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Rixian.Extensions.AspNetCore.DataProtection.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Rixian.Extensions.AspNetCore.DataProtection.Properties.Resources", typeof(Resources).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [DATA_PROTECTION] Configuration found, enabling Data Protection. ApplicationDiscriminator: {ApplicationDiscriminator}, KeyRing_KeyIdentifier: {KeyRing_KeyIdentifier}, KeyRing_KeyName: {KeyRing_KeyName}.
        /// </summary>
        internal static string ConfigurationFoundMessage {
            get {
                return ResourceManager.GetString("ConfigurationFoundMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [DATA_PROTECTION] No configuration section named &apos;DataProtection&apos; found, and running in Development. DataProtection will not be enabled..
        /// </summary>
        internal static string ConfigurationMissingMessage {
            get {
                return ResourceManager.GetString("ConfigurationMissingMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DataProtection credentials must be provided for non-Development applications..
        /// </summary>
        internal static string CredentialsRequiredMessage {
            get {
                return ResourceManager.GetString("CredentialsRequiredMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [DATA_PROTECTION] Invalid configuration specified, and running in Development. Local-only DataProtection will be enabled. {Error}.
        /// </summary>
        internal static string InvalidConfigurationMessage {
            get {
                return ResourceManager.GetString("InvalidConfigurationMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to KeyRing must be provided..
        /// </summary>
        internal static string MissingKeyRingErrorMessage {
            get {
                return ResourceManager.GetString("MissingKeyRingErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A value must be provided..
        /// </summary>
        internal static string MissingValueErrorMessage {
            get {
                return ResourceManager.GetString("MissingValueErrorMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [DATA_PROTECTION] No configuration section named &apos;DataProtection&apos; found, and running in as non-Development. DataProtection credentials must be provided for non-Development applications..
        /// </summary>
        internal static string RequiredConfigurationMissingMessage {
            get {
                return ResourceManager.GetString("RequiredConfigurationMissingMessage", resourceCulture);
            }
        }
    }
}
