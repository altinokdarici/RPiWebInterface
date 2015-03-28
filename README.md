# RPiWebInterface
Raspberry Pi 2 GPIO Web Interface (one of my hobby project)

#Client Config
##create RPiClient folder under /home/pi
sudo mkdir /home/pi/RPiClient

##create update script
cd /home/pi/RPiClient
sudo nano update
rm -f *.*
wget http://www.altinokdarici.com/RPi/RPiClient.tar
tar -xf RPiClient

run the below script to download or update client program
sudo bash /home/pi/RPiClient/update

##Auto start
<pre>
sudo nano /etc/init.d/RPiClient
case "$1" in
  start)
    echo "starting"
    sudo /home/pi/RPiClient/RPi.Client.exe > /home/pi/RPiClient/output 2> /home/pi/RPiClient/error
    ;;
  stop)
    echo "stopping"
    killall RPi.Client.exe
    ;;
  *)
    echo "Usage: /etc/init.d/RPiClient {start|stop}"
    exit 1
    ;;
esac
 
exit 0
</pre>

###Register for auto start
sudo chmod +x /etc/init.d/RPiClient
sudo update-rc.d RPiClient defaults
