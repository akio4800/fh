package com.company;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;


public class Main {

    public static int findRun(ImageProcessor ip, int x_start, int y) {

        int val = ip.getPixel(x_start, y);
        int w = ip.getWidth();
        while (x_start < w - 1) {
            if (val == ip.getPixel(x_start + 1, y)) {
                x_start++;
            } else {
                return x_start + 1;
            }
        }
        return x_start + 1;
    }


   public static List<Integer> encodeRun(int lengh) {
        List<Integer> list = new ArrayList<>();

        while (lengh > 254) {
            list.add(254);
            list.add(0);
            lengh -= 254;
        }
        list.add(lengh);
        return list;


    }


  public  static List<Integer> encode(ImageProcessor ip) {

        List<Integer> list = new ArrayList<>();
        int w = ip.getWidth();
        int h = ip.getHeight();

        for (int y = 0; y < h; y++) {


            int x = 0;
            if (ip.getPixel(x, y) == 1) {
                list.add(0);
            }
            while (x < w) {
                int x_end = findRun(ip, x, y);
            //    markRun(ip, x, x_end, y);
                list.addAll(encodeRun(x_end - x));
                x = x_end;

            }
            list.add(255);
        }


        return list;
    }




  public static   void decode( List<Integer> code){


        int height = Collections.frequency(code, 255);

        int width = 0;
        int i = 0;
        while (code.get(i) != 255){
            width+= code.get(i);
            i++;

        }

        System.out.println(height +  " | " + width);



        ImageProcessor ip2 = new ImageProcessor(width,height);

        int pos = 0;
        int it = 0;

        boolean color = true;

        for (int j = 0; j < height; j++) {
            for (int k = 0; k < width ; k++) {
                if (it == 0 && code.get(i) == 0 || code.get(i) == 0 && code.get(i - 1) == 255) {
                    color = !color;
                }
                int val =  code.get(pos);
                while (val == 0 || val == 255){

                       pos++;
                       val = code.get(pos);

                }
                val = code.get(pos);
                if(it < val){
                    it ++;
                }
                if (color) {
                    System.out.print(1 +" ");
                    // ip2.putPixel(j, k, 255);
                }else{
                    System.out.print(0 +" ");
                    //   ip2.putPixel(j, k, 0);
                }
                if (it == val){
                    if (!(it == 254)) {
                        color = !color;
                    }
                    pos++;
                    it = 0;
                }

            }
            System.out.println();
        }



             //   ip2.print();
        //  new ImagePlus("t",ip2).show();

    }


   public static void printCode(List<Integer> list) {

        for (int i : list) {
            System.out.print(i + " ");
        }
        System.out.println();
    }

   public static void markRun(ImageProcessor ip, int x_start, int x_end, int y) {

        for (int i = x_start; i < x_end; i++) {
            if (ip.getPixel(i, y) > 0) {

                ip.putPixel(i, y, 200);
            } else {
                ip.putPixel(i, y, 100);
            }

        }


    }

   public static void invert(ImageProcessor ip, int h, int w) {
        for (int x = 0; x < w; x++) {
            for (int y = 0; y < h; y++) {
                int val = ip.getPixel(x, y);    // get pixel value at (x,y)
                ip.putPixel(x, y, 255 - val);    // set pixel value at (x,y)
            }
        }
    }


    public static void main(String[] args) {



ImageProcessor ip = new ImageProcessor();
ip.add();


      List<Integer>  l =  encode(ip);

        printCode(l);

        decode(l);

    }



}




