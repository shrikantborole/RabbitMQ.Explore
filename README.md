# RabbitMQ.Explore

Steps to use:

Steps to use:
A) Clone the code.
B) Update the appsetting.config with proper settings.
	//Test without SSL Enabled
	  "MTLSEnabled": "false",
           "SSLEnabled": "false"
	//SSL Enabled, but TLS is off
	  "MTLSEnabled": "false",
           "SSLEnabled": "true"
	//SSL Enabled, and TLS is on
	    "MTLSEnabled": "true",
            "SSLEnabled": "true"
C) Copy the certificate at %APPDATA%\RabbitMQ\ and update the RabbitMQ Config file.
	1. In case if you wnat to generate the certificate on you own machine, please checked Certiciate/ReadMe.txt
D) Copy the rabbitmq-02.conf at %APPDATA%\RabbitMQ\ and rename it to rabbitmq.conf
	1. Update the path properly in the config, please check ConfigFile/ReadMe.txt
E) Restart the service and run the code. Ideally this should work.

Note:
The p12 file contains BOTH the client cert and key. So we need to pass it in property:
   factory.Ssl.CertPath = certificateFilePath;
For more information: https://groups.google.com/g/rabbitmq-users/c/Xd9vkBXK3ww
