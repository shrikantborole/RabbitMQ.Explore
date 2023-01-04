Steps to use:
A) Clone the code.
B) Update the appsetting.config with proper settings.
C) Copy the certificate at %APPDATA%\RabbitMQ\
	1. In case if you wnat to generate the certificate on you own machine, please checked Certiciate/ReadMe.txt
D) Copy the rabbitmq-02.conf at %APPDATA%\RabbitMQ\ and rename it to rabbitmq.conf
	1. Update the path properly in the config, please check ConfigFile/ReadMe.txt
E) Restart the service and run the code. Ideally this should work.