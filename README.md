# Sample API for My Own Blogger

Sample of create a MyOwnBlog API

## Using Docker

Build image<br/>
```docker build -t my-own-blog .```

Create container base on image we have created<br/>
```docker run -dp 8092:8092 --name my-blog my-own-blog```
