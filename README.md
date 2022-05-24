# ProjectTOTP

The project code is structured as follows:

1.Starting point - WebPage - Web.UI.Page logic

2.Backend logic  - contains TOTP Algorithm Logic dev and also UserData class for input data processing
	(* design all the logic based on dependency injection principle using AutoFac)
	
3.Separate project for unit testing
	(* using NUnit.Framework and Moq)

Note!!

If you run the project and you have (HTTP Error 403.14 - Forbidden - The Web server is configured to not list the contents of this directory), please set WrbPage.aspx as 
start page and run it again! 
