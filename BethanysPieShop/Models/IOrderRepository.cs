﻿using System;
namespace BethanysPieShop.Models
{
	public interface IOrderRepository
	{
		void CreateOrder(Order Order);
	}
}

