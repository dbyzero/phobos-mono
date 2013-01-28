using System;
using System.Collections;
using System.IO;
using System.Reflection;
using Microsoft.Win32;

namespace Phobos.Configurations.Keyboard
{
    class WindowsKeyboardExtractor : AKeyboardLayoutExtractor
    {
        
        
        public override string getCurrentSystemLayout()
        {
            //identificateur des paramètres régionaux (LCID) 
            String lcdi = (string)Registry.GetValue("HKEY_CURRENT_USER\\Keyboard Layout\\Preload", "1", null);
            //0 + lcdi.Substring(1,lcdi.Length-1) car le lcid commence par 'd' et il nous faut 8 caractères dont le premier et 0 (lcdi - le d contient 7 char);
            return (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Keyboard Layouts\\0" + lcdi.Substring(1, lcdi.Length - 1), "Layout Text", AKeyboardLayoutExtractor.DEFAULT_LAYOUT);
        }
    }
}