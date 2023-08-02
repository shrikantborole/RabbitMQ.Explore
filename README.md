# RabbitMQ.Explore
**Steps to use:**
1. Clone the code.
2. Update the appsetting.config with proper settings.

	//Test without SSL Enabled\
		"MTLSEnabled": "false"\
    	"SSLEnabled": "false"\
	//SSL Enabled, but TLS is off\
		"MTLSEnabled": "false",\
		"SSLEnabled": "true"\
	//SSL Enabled, and TLS is on\
		"MTLSEnabled": "true",\
        "SSLEnabled": "true"

3. Copy the certificate at %APPDATA%\RabbitMQ\ and update the RabbitMQ Config file.
   * In case if you wnat to generate the certificate on you own machine, please checked Certiciate/ReadMe.txt
4. Copy the rabbitmq-02.conf at %APPDATA%\RabbitMQ\ and rename it to rabbitmq.conf
	* Update the path properly in the config, please check ConfigFile/ReadMe.txt

5. Restart the service and run the code. Ideally this should work.

**Note:**\
The p12 file contains BOTH the client cert and key. So we need to pass it in property:\
   factory.Ssl.CertPath = certificateFilePath;\
For more information: https://groups.google.com/g/rabbitmq-users/c/Xd9vkBXK3ww
