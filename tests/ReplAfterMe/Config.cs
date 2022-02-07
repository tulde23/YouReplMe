using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplAfterMe
{
    public static  class Config
    {
        public static string WritePrompt()
        {
            return $"search: ";
        }
        public static string WriteLogo()
        {
            return @"


########  ######## ########  ##             ###    ######## ######## ######## ########     ##     ## ######## 
##     ## ##       ##     ## ##            ## ##   ##          ##    ##       ##     ##    ###   ### ##       
##     ## ##       ##     ## ##           ##   ##  ##          ##    ##       ##     ##    #### #### ##       
########  ######   ########  ##          ##     ## ######      ##    ######   ########     ## ### ## ######   
##   ##   ##       ##        ##          ######### ##          ##    ##       ##   ##      ##     ## ##       
##    ##  ##       ##        ##          ##     ## ##          ##    ##       ##    ##     ##     ## ##       
##     ## ######## ##        ########    ##     ## ##          ##    ######## ##     ##    ##     ## ######## 

                                                                        

";
        }
    }
}
