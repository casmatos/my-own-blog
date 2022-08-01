# Sample API for My Own Blogger

Sample of create a MyOwnBlog API

## Using Docker

Build image<br/>
```docker build -t my-own-blog .```

Create container base on image we have created<br/>

Define Environment to run on Docker and define SQL Server host (my Machine)<br/>
```docker run -dp 7050:80 -e SQL_SERVER_CONNECTION_STRING="Server=host.docker.internal,1433;Database=MyOwnBlog;User Id=sa;Password=[Password]" --name my-blog my-own-blog```

Define Environment to run on Docker and define SQL Server host (Remote)<br/>
```docker run -dp 7050:80 -e SQL_SERVER_CONNECTION_STRING="Server=[IP/Hostname],1433;Database=MyOwnBlog;User Id=[Username];Password=[Password]" --name my-blog my-own-blog```