﻿using System;
using System.Collections.Generic;
using MAD.Plugin.MessagingService.Core;

namespace MaterialTest
{
	public class BearsMessage : IMessage
	{
		public readonly IEnumerable<Bears> bears;

		public BearsMessage(IEnumerable<Bears> bears)
		{
			this.bears = bears;
		}
	}
}

