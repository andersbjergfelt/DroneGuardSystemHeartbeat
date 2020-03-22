Prerequisites:
Java
.NET Core
(Linux 'watch' command if you want to run the bash script "DroneGuardToDrone.sh" (it is not needed))


To run the first implementation:

- Go into the DroneGuardSystem folder and type 'dotnet run' in the terminal

- Go into src/com.KCS7/ compile and run HeartbeatResponse.java program (Easier if you use an IDE)

- Go to localhost:5000/healthcheck and confirm from the output that the drone is "alive"


To run the second implementation:

- Go into the DroneGuardSystem folder and type 'dotnet run' in the terminal

- Go into src/com.KCS7/ compile and run HeartbeatRequest.java program (Easier if you use an IDE)

- Go to the terminal where you started the dotnet run and confirm that it receives request from the Java program.






