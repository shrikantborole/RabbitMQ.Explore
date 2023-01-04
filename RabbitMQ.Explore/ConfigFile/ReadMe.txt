FYI:
a. On the server end, peer verification is primarily controlled using two configuration options: ssl_options.verify and ssl_options.fail_if_no_peer_cert.
b. Setting the ssl_options.fail_if_no_peer_cert option to false tells the node to accept clients which don't present a certificate (for example, were not configured to use one).
c. When the ssl_options.verify option is set to verify_peer, the client does send us a certificate, the node must perform peer verification. When set to verify_none, peer verification will be disabled and certificate exchange won't be performed.

Old style configuration:
advanced-01 => Disabled MTLS
advanced-02 => Enable MTLS


New Style configuration:
rabbitmq-01 => Enabled MTLS
rabbitmq-01 => Disabled MTLS

Note: 
A) It's recommended to use New Style Configurtion.
B) Add the rabbitmq-01.conf file that is placed at /ConfigFile, and put it in the %APPDATA%\RabbitMQ\ directory
C) Put your new certificates OR existing one from repository path /Certiciate/tks-gen in the  %APPDATA%\RabbitMQ\ directory. Ensure that the files are named the same as what is specified in the rabbitmq.conf file.
D) Open an administrative command prompt and navigate to C:\Program Files\RabbitMQ Server\rabbitmq*\sbin
E) Stop the Windows service: .\rabbitmq-service.bat stop
F) Ensure that RabbitMQ starts correctly. If not, copy all of the console output to a file. Attach that and your log file to your next response.
G) If RabbitMQ starts correctly, you can remove the log.* lines in the rabbitmq.conf file or change debug to info
H) You can stop RabbitMQ using CTRL-C, or open another admin console, navigate to the sbin dir, and run .\rabbitmqctl.bat shutdown
I) Re-start the Windows service:  .\rabbitmq-service.bat start

NOTE:
A) First Try with diabling the MTLS.
B) In case if you are setting the property:
	ssl_options.fail_if_no_peer_cert = false, then 
	set the  "MTLSEnabled": "false" in appsetting.config