# URL Shorter
A small server and client application to shorten URLs
## Installing
So at first on both client and server install .Net Core 2. Then clone the project and build it.
## Running
### Fast running
#### Server
```bash
./server
```
_Also open the 3303/tcp port on your firewall._
#### Client
```bash
./client short -s YOUR_SERVER_IP -u https://github.com/HirbodBehnam/URL-Shorter
```
### Detailed Usage
#### Server
Here is the options for server:
```
  -v, --verbose    Set output to verbose messages.

  --db             Path to the database file.

  -p, --port       (Default: 3303) Set the port that server must listen on it.

  -b, --bind       (Default: 0.0.0.0) Set the IP to listen on.

  --password       Sets a password for server. Anyone who wants to *SHORTEN URL* will be asked for password.

  --help           Display this help screen.

  --version        Display version information.
```
Just a small note about the password: People can still see shorten links _without_ password. It's is only required when you want to shorten a url.
#### Client
In client you have 2 modes
##### Shorten URL
If you want to shorten URL use this pattern:
```
  -u, --url        Required. URL to shorten

  -s, --server     Required. The server to connect to.

  -p, --port       (Default: 3303) The port to connect to for server.

  -v, --verbose    Set output to verbose messages.

  --password       The password for server. [If needed]

  --help           Display this help screen.

  --version        Display version information.
```
Example:
```
./client short -s 1.1.1.1 -u https://github.com/HirbodBehnam/URL-Shorter -p 33033 --password 1234
```
With this command, client connects to 1.1.1.1:33033 with `1234` password and asks the server to shorten the `https://github.com/HirbodBehnam/URL-Shorter` url.
##### Decode Shorted URL
If you want to know that is the url for a token, here is the help:
```
  -u, --url        Required. URL or token to decode

  -s, --server     Required. The server to connect to.

  -p, --port       (Default: 3303) The port to connect to for server.

  -v, --verbose    Set output to verbose messages.

  --help           Display this help screen.

  --version        Display version information.
```
Example:
```
./client decode -u GI -s 1.1.1.1
```
With this command, connects to 1.1.1.1:3303 and trys to search the database for `GI`. If found it will return the url for that.
