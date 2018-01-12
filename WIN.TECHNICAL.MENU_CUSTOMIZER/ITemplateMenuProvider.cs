using System;
using System.Collections.Generic;
using System.Text;

namespace WIN.TECHNICAL.MENU_CUSTOMIZER
{
    public interface ITemplateMenuProvider
    {
        MenuRepresentation CreateApplicationMenuRepresentation();
    }
}
