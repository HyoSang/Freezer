package com.example.light.freezer;

import android.app.Activity;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.util.Log;

import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URL;

/**
 * Created by YunSang on 2017-06-03.
 */

public class Post {
    public static String POST(String url, Person person){
        InputStream is = null;
        String result = "";
        try {
            URL urlCon = new URL(url);
            HttpURLConnection httpCon = (HttpURLConnection)urlCon.openConnection();

            String json = "";

            // 3. build jsonObject
            JSONObject jsonObject = new JSONObject();
            jsonObject.accumulate("ID", person.getID());
            jsonObject.accumulate("Pass", person.getPass());

            // 4. convert JSONObject to JSON to String
            json = jsonObject.toString();

            // ** Alternative way to convert Person object to JSON string usin Jackson Lib
            // ObjectMapper mapper = new ObjectMapper();
            // json = mapper.writeValueAsString(person);

            // 5. set json to StringEntity
            httpCon.setRequestProperty("Accept","application/json");
            httpCon.setRequestProperty("Content-type","application/json");


            // 8. Execute POST request to the given URL
            httpCon.setDoOutput(true);

            // 9. receive response as inputStream
            httpCon.setDoInput(true);

            // 10. convert inputstream to string

            OutputStream os = httpCon.getOutputStream();
            os.write(json.getBytes("UTF-8"));
            os.flush();

            try{
                is = httpCon.getInputStream();
                if(is != null)
                    result = convertInputStreamToString(is);
                else
                    result = "Did not work!";
            }catch(IOException e){
                e.printStackTrace();
            }
            finally {
                httpCon.disconnect();
            }

        }
        catch(IOException e){
            e.printStackTrace();
        }
        catch (Exception e) {
            Log.d("InputStream", e.getLocalizedMessage());
        }

        // 11. return result
        return result;
    }

    private static String convertInputStreamToString(InputStream inputStream) throws IOException {
        BufferedReader bufferedReader = new BufferedReader( new InputStreamReader(inputStream));
        String line = "";
        String result = "";
        while((line = bufferedReader.readLine()) != null)
            result += line;

        inputStream.close();
        return result;

    }
}
