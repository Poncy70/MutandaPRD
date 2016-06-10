﻿// ***********************************************************************
// Assembly         : XLabs.Forms
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="CustomRadioButton.cs" company="XLabs Team">
//     Copyright (c) XLabs Team. All rights reserved.
// </copyright>
// <summary>
//       This project is licensed under the Apache 2.0 license
//       https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/LICENSE
//       
//       XLabs is a open source project that aims to provide a powerfull and cross 
//       platform set of controls tailored to work with Xamarin Forms.
// </summary>
// ***********************************************************************
// 

using System;
using Xamarin.Forms;

namespace Mutanda.Extensions
{
	/// <summary>
	/// Class CustomRadioButton.
	/// </summary>
	//public class CustomRadioButton : View
	//{
	//	/// <summary>
	//	/// The checked property
	//	/// </summary>
	//	public static readonly BindableProperty CheckedProperty = BindableProperty.Create<CustomRadioButton, bool>(p => p.Checked, false);

	//	/// <summary>
	//	///     The default text property.
	//	/// </summary>
	//	public static readonly BindableProperty TextProperty = BindableProperty.Create<CustomRadioButton, string>(p => p.Text, string.Empty);

	//	/// <summary>
	//	///     The default text property.
	//	/// </summary>
	//	public static readonly BindableProperty TextColorProperty = BindableProperty.Create<CustomRadioButton, Color>(p => p.TextColor, Color.Default);

	//	/// <summary>
	//	/// The font size property
	//	/// </summary>
	//	public static readonly BindableProperty FontSizeProperty = BindableProperty.Create<CheckBox, double>(p => p.FontSize, -1);

	//	/// <summary>
	//	/// The font name property.
	//	/// </summary>
	//	public static readonly BindableProperty FontNameProperty = BindableProperty.Create<CheckBox, string>(p => p.FontName, string.Empty);

	//	/// <summary>
	//	///     The checked changed event.
	//	/// </summary>
	//	public EventHandler<EventArgs<bool>> CheckedChanged;

	//	/// <summary>
	//	///     Gets or sets a value indicating whether the control is checked.
	//	/// </summary>
	//	/// <value>The checked state.</value>
	//	public bool Checked
	//	{
	//		get { return this.GetValue<bool>(CheckedProperty); }

	//		set
	//		{
	//			SetValue(CheckedProperty, value);

	//			var eventHandler = CheckedChanged;

	//			if (eventHandler != null)
	//			{
	//				eventHandler.Invoke(this, value);
	//			}
	//		}
	//	}

	//	/// <summary>
	//	/// Gets or sets the text.
	//	/// </summary>
	//	/// <value>The text.</value>
	//	public string Text
	//	{
	//		get { return this.GetValue<string>(TextProperty); }

	//		set { SetValue(TextProperty, value); }
	//	}

	//	/// <summary>
	//	/// Gets or sets the color of the text.
	//	/// </summary>
	//	/// <value>The color of the text.</value>
	//	public Color TextColor
	//	{
	//		get { return this.GetValue<Color>(TextColorProperty); }

	//		set { SetValue(TextColorProperty, value); }
	//	}

	//	/// <summary>
	//	/// Gets or sets the size of the font.
	//	/// </summary>
	//	/// <value>The size of the font.</value>
	//	public double FontSize
	//	{
	//		get
	//		{
	//			return (double)GetValue(FontSizeProperty);
	//		}
	//		set
	//		{
	//			SetValue(FontSizeProperty, value);
	//		}
	//	}

	//	/// <summary>
	//	/// Gets or sets the name of the font.
	//	/// </summary>
	//	/// <value>The name of the font.</value>
	//	public string FontName
	//	{
	//		get
	//		{
	//			return (string)GetValue(FontNameProperty);
	//		}
	//		set
	//		{
	//			SetValue(FontNameProperty, value);
	//		}
	//	}

	//	#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
	//	/// <summary>
	//	/// Gets or sets the identifier.
	//	/// </summary>
	//	/// <value>The identifier.</value>
	//	public int Id { get; set; }
	//	#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
	//}

	//public static class BindableObjectExtensions
	//{
	//	/// <summary>
	//	/// Gets the value.
	//	/// </summary>
	//	/// <typeparam name="T"></typeparam>
	//	/// <param name="bindableObject">The bindable object.</param>
	//	/// <param name="property">The property.</param>
	//	/// <returns>T.</returns>
	//	public static T GetValue<T>(this BindableObject bindableObject, BindableProperty property)
	//	{
	//		return (T)bindableObject.GetValue(property);
	//	}
	//}

	//public static class EventExtensions
	//{
	//	/// <summary>
	//	/// Raise the specified event.
	//	/// </summary>
	//	/// <param name="handler">Event handler.</param>
	//	/// <param name="sender">Sender object.</param>
	//	/// <param name="value">Argument value.</param>
	//	/// <typeparam name="T">The value type.</typeparam>
	//	public static void Invoke<T>(this EventHandler<EventArgs<T>> handler, object sender, T value)
	//	{
	//		var handle = handler;
	//		if (handle != null)
	//		{
	//			handle(sender, new EventArgs<T>(value));
	//		}
	//	}

	//	/// <summary>
	//	/// Tries the invoke.
	//	/// </summary>
	//	/// <typeparam name="T"></typeparam>
	//	/// <param name="handler">The handler.</param>
	//	/// <param name="sender">The sender.</param>
	//	/// <param name="args">The arguments.</param>
	//	/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
	//	public static bool TryInvoke<T>(this EventHandler<T> handler, object sender, T args) where T : EventArgs
	//	{
	//		var handle = handler;
	//		if (handle == null) return false;

	//		handle(sender, args);
	//		return true;
	//	}
	//}
}