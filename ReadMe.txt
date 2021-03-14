1. How to get signature hash from command line?

keytool -list -alias medicineregistry -keystore medicineregistry.keystore | openssl sha1 -binary | openssl base64