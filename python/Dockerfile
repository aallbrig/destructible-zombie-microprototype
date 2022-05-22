# Install all requirements for https://pypi.org/project/bpy/
FROM ubuntu:latest

# install python 3.7 (for compatibility with bpy)
RUN apt update \
    && apt install wget libffi-dev libssl-dev openssl make gcc zlib1g-dev git subversion cmake -y

RUN cd /opt \
    && wget https://www.python.org/ftp/python/3.7.9/Python-3.7.9.tgz \
    && tar xzvf Python-3.7.9.tgz \
    && cd Python-3.7.9 \
    && ./configure \
    && make \
    && make install \
    && ln -fs /opt/Python-3.7.9/Python /usr/bin/python3

# which includes installing blender https://wiki.blender.org/wiki/Building_Blender/Linux/Ubuntu
RUN apt install blender -y

WORKDIR /workspace

# requirements which strangely aren't considered in pip freeze
RUN python3 -m pip install wheel
ADD . .
RUN python3 -m pip install -r requirements.txt