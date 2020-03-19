#!/bin/bash

watch -n 5 "curl --raw http://localhost:5000/healthcheck"
