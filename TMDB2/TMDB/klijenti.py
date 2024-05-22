import http.client
import threading

def send_request():
    try:
        conn = http.client.HTTPConnection("localhost", 5500)
        
        conn.request("GET", "/search/Titanic")
        
        response = conn.getresponse()
        print(f"Response from server: {response.status}")
        
        conn.close()
    except Exception as e:
        print(f"Error: {e}")

num_clients = 100

threads = []

for i in range(num_clients):
    thread = threading.Thread(target=send_request)
    thread.start()
    threads.append(thread)

for thread in threads:
    thread.join()

print("All requests sent.")
