docker build -t king .

docker stop king_container_1
docker rm king_container_1

docker run -it -p 5000:8080 --name king_container_1 king
