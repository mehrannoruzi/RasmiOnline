namespace RasmiOnline.Business.Protocol
{
    using Domain.Entity;
    using Gnu.Framework.Core;
    using System;
    using System.Collections.Generic;

    public interface IChannelBusiness
    {
        IActionResponse<int> Insert(Channel model);
        IActionResponse<int> Update(Channel model);
        IActionResponse<int> UpdateClick(string refrence);
        IEnumerable<Channel> GetAll();
        IEnumerable<Channel> GetAllUserChannel(Guid userId);
        IActionResponse<Channel> Find(int channelId);
    }
}
