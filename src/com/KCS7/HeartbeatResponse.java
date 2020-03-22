package com.KCS7;

import java.io.*;
import java.lang.management.ManagementFactory;
import java.net.ServerSocket;
import java.net.Socket;

import com.sun.management.OperatingSystemMXBean;
import com.sun.management.UnixOperatingSystemMXBean;

public class HeartbeatResponse {




    public static void heartBeatResponse() throws IOException, InterruptedException {

        OperatingSystemMXBean osBean = (UnixOperatingSystemMXBean) ManagementFactory.getOperatingSystemMXBean();
            try (
                    //this equals localhost:6789
                    ServerSocket aServer = new ServerSocket(6789);
                    Socket cn = aServer.accept();
                    BufferedReader bis = new BufferedReader(new InputStreamReader(cn.getInputStream()));
                    BufferedOutputStream bos = new BufferedOutputStream(cn.getOutputStream());)
            {
                String line = bis.readLine();
                while(line != null && !line.equals(""))
                {
                    line = bis.readLine();
                }
                byte[] message="200 OK".getBytes();

                bos.write("HTTP/1.1 200 OK\r\n".getBytes());
                bos.write("Content-Type: text/plain\r\n".getBytes());
                bos.write(("Content-Length: "+message.length+"\r\n").getBytes());
                bos.write("\r\n".getBytes()); // empty line between HTTP header and HTTP content
                bos.write(message);

            } catch (IOException ex) {
                System.out.println("Error in connnection: " + ex.getMessage());
            }

            System.out.println(osBean.getProcessCpuLoad() * 100);
        }


    public static void main(String[] args) throws IOException, InterruptedException {

        while(true)
        {

            heartBeatResponse();
            //Thread.sleep(5000);
        }


    }

    }