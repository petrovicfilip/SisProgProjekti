import http.client
import threading

line = 0
mutex = threading.Lock()

def send_request():
    global line
    try:
        conn = http.client.HTTPConnection("localhost", 5500)
        beg = 'C:\\SistemskoProgramiranjeGitHub\\SisProgProjekti\\TMDB2\\TMDB\\'
        with open(beg + 'filmovi.txt', 'r') as file:
            with mutex:
                for _ in range(line):
                    file.readline()
                movie = file.readline()
                line += 1
                line %= 50

        conn.request("GET", "/search/" + movie.strip())

        response = conn.getresponse()
        print(f"Response from server: {response.status}")

        conn.close()
    except Exception as e:
        print(f"Error: {e}")

num_clients = 200

threads = []

for i in range(num_clients):
    thread = threading.Thread(target=send_request)
    thread.start()
    threads.append(thread)

for thread in threads:
    thread.join()

print("All requests sent.")
