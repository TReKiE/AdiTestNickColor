using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdiIRCAPIv2;
using AdiIRCAPIv2.Enumerators;
using AdiIRCAPIv2.Interfaces;

namespace AdiTestNickColor
{
    public class TestingNickColor : IPlugin
    {
        public string PluginName { get { return "AdiTestNickColor"; } }

        public string PluginDescription { get { return "Testing AdiIRC RgbNickColor."; } }

        public string PluginAuthor { get { return "Jonathan Kay"; } }

        public string PluginVersion { get { return "1.0"; } }

        public string PluginEmail { get { return ""; } }

        public IPluginHost host;

        public void Initialize(IPluginHost pluginHost)
        {
            host = pluginHost;
            pluginHost.OnChannelNormalMessage += PluginHost_OnChannelNormalMessage;
        }

        private void PluginHost_OnChannelNormalMessage(AdiIRCAPIv2.Arguments.ChannelMessages.ChannelNormalMessageArgs argument)
        {
            string color = argument.Channel.Evaluate($"$rgb($nick({argument.Channel.Name},{argument.User.Nick}).rgbcolor).hex", "");
            argument.Channel.ExecuteCommand($"/echo -g {argument.Channel.Name} NickColor: {argument.User.NickColor}");
            argument.Channel.ExecuteCommand($"/echo -g {argument.Channel.Name} RgbNickColor: {argument.User.RgbNickColor}");
            argument.Channel.ExecuteCommand($"/echo -g {argument.Channel.Name} Evaluated Color: {color}");
            argument.Channel.ExecuteCommand($"//echo -g {argument.Channel.Name} $chr(4) $+ {color} hello!");
        }

        public void Dispose()
        {
            
        }
    }
}
