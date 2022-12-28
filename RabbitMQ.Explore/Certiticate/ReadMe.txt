Steps to create Certificate:

Note: Don't give password to the cetificate. 
If you have a password, you can not run RabbitMQ as a Windows service because it will prompt you for a password when it starts, and it does not start interactively as a service.

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

openssl req -new -key private_key.pem -out req.pem -outform PEM -subj /CN=desktop-s08pnk3/O=server/ -nodes

cd..

openssl ca -config openssl.cnf -in ./server/req.pem -out ./server/server_certificate.pem -notext -batch -extensions server_ca_extensions

openssl pkcs12 -export -out ./server/server_certificate.p12 -in ./server/server_certificate.pem -inkey ./server/private_key.pem -passout pass:MySecretPassword

mkdir client

cd client

openssl genrsa -out private_key.pem 2048

openssl req -new -key private_key.pem -out req.pem -outform PEM -subj /CN=desktop-s08pnk3/O=client/ -nodes

cd..

openssl ca -config openssl.cnf -in ./client/req.pem -out ./client/client_certificate.pem -notext -batch -extensions client_ca_extensions

openssl pkcs12 -export -out ./client/client_certificate.p12 -in ./client/client_certificate.pem -inkey ./client/private_key.pem -passout pass:MySecretPassword