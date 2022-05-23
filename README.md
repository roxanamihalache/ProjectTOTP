# ProjectTOTP

The project code is structured as follows:
1.Starting point - WebPage - Web.UI.Page logic
2.Backend logic  - contains TOTP Algorithm Logic dev and also UserData class for input data processing
	* design all the logic based on dependency injection principle using AutoFac
3.Separate project for unit testing
	* using NUnit.Framework and Moq
