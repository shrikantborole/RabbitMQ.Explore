﻿Steps to create Certificate:

Note: Don't give password to the cetificate. 
A) If you have a password, you can not run RabbitMQ as a Windows service because it will prompt you for a password when it starts, and it does not start interactively as a service.
B) Install Openssl - https://wiki.openssl.org/index.php/Binaries. Set the Env path for openssl. Navigate to openssl for the commands.
C) FYI: The certificate with this repo should work with your working machin, in case if you want to avoid to create the certificates.
Section A: For LINUX and UBUNTU
Section B: For WINDOWS

Section A: For LINUX and UBUNTU
-------------------------------

This certificate will be used in case if you have configured your RabbitMQ on LINUX or Ubuntu.
Rffer URL: https://www.rabbitmq.com/ssl.html
Section Name: Manually Generating a CA, Certificates and Private Keys

Step1: 
Bash Cmd Prompt:

cd /C/temp/ThirdOne
mkdir testca
cd testca
mkdir certs private
chmod 700 private
echo 01 > serial
touch index.txt

Step2:
Using OpenSSL CMD

openssl req -x509 -config openssl.cnf -newkey rsa:2048 -days 365 -out ca_certificate.pem -outform PEM -subj /CN=MyTestCA/ -nodes

openssl x509 -in ca_certificate.pem -out ca_certificate.cer -outform DER

mkdir server

cd server

openssl genrsa -out private_key.pem 2048

openssl req -new -key private_key.pem -out req.pem -outform PEM -subj /CN=PNQ1-LP98R10J3/O=server/ -nodes

cd..

openssl ca -config openssl.cnf -in ./server/req.pem -out ./server/server_certificate.pem -notext -batch -extensions server_ca_extensions

openssl pkcs12 -export -out ./server/server_certificate.p12 -in ./server/server_certificate.pem -inkey ./server/private_key.pem -passout pass:MySecretPassword

mkdir client

cd client

openssl genrsa -out private_key.pem 2048

openssl req -new -key private_key.pem -out req.pem -outform PEM -subj /CN=PNQ1-LP98R10J3/O=client/ -nodes

cd..

openssl ca -config openssl.cnf -in ./client/req.pem -out ./client/client_certificate.pem -notext -batch -extensions client_ca_extensions

openssl pkcs12 -export -out ./client/client_certificate.p12 -in ./client/client_certificate.pem -inkey ./client/private_key.pem -passout pass:MySecretPassword


Section B: For WINDOWS

A) For Windows, it's recommended by rabbitMQ tuitorial to use: tls-gen https://github.com/rabbitmq/tls-gen
B) Inorder to create certificate on windows you will need below software:
	A) GNU make (once chocolatey (https://chocolatey.org/install) is installed, use command: choco install make - https://community.chocolatey.org/packages/make)
	B) Python - https://www.python.org/downloads/, you can install it using chocolatey (https://chocolatey.org/install) on WINDOWS.
	C) Set ENV path for all the software (GNU make, Python and chocolatey) and check for version using command prompt.
C) Once this is done, clone the project tls-gen and execute below command.
	git clone https://github.com/rabbitmq/tls-gen.git
	cd tls-gen/basic
	make PYTHON=python
D) This will create the certificates in the result folder i.e. /basic/result
