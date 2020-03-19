package com.KCS7;


import com.sun.management.OperatingSystemMXBean;
import com.sun.management.UnixOperatingSystemMXBean;

import java.io.*;
import java.lang.management.ManagementFactory;
import java.net.*;


public class HeartbeatRequest {


    public static void heartBeatRequest() throws MalformedURLException {

        HttpURLConnection connection = null;
        String USER_AGENT = "Mozilla/5.0";
        String  targetURL = "http://localhost:5000/drone";

        OperatingSystemMXBean osBean = (UnixOperatingSystemMXBean) ManagementFactory.getOperatingSystemMXBean();
        try {

            //Create connection
            URL url = new URL(targetURL);
            connection = (HttpURLConnection) url.openConnection();
            connection.setRequestMethod("GET");
            connection.setRequestProperty("User-Agent", USER_AGENT);
            connection.setDoOutput(true);

            //Get Response
            BufferedReader in = new BufferedReader(
                    new InputStreamReader(connection.getInputStream()));
            String inputLine;
            StringBuffer response = new StringBuffer();
            int responseCode = connection.getResponseCode();
            System.out.println("\nSending 'GET' request to URL : " + url);
            System.out.println("Response Code : " + responseCode);

            while ((inputLine = in.readLine()) != null) {
                response.append(inputLine);
            }
            in.close();
            System.out.println(response.toString());

        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            if (connection != null) {
                connection.disconnect();
            }
            System.out.println(osBean.getProcessCpuLoad() * 100);

        }

    }


    public static void main(String[] args) throws IOException, InterruptedException {

        while (true) {
            heartBeatRequest();
            Thread.sleep(5000);
        }
    }
}
