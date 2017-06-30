package com.company;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

/**
 * Created by akio on 29.04.2017.
 */
public class ImageProcessor {


      ArrayList<ArrayList<Integer>> list = new ArrayList<>();
      int width = 0;
      int height = 0;

    public ImageProcessor(){}

    public ImageProcessor(int width, int height) {

        for (int i = 0; i < height; i++) {
            ArrayList<Integer> l = new ArrayList<>();
            for (int j = 0; j <width; j++) {
                l.add(0);
            }
           list.add(l);
        }
    }

    public  void add(){


        list.add(new ArrayList<>(Arrays.asList(1,1,1,0,0,1,1,1,1,1)));
        list.add(new ArrayList<>(Arrays.asList(1,1,1,1,0,0,1,1,1,1)));
        list.add(new ArrayList<>(Arrays.asList(1,1,1,0,0,0,0,1,0,1)));
        width = list.get(0).size();
        height = list.size();


    }



    public int getPixel(int x, int y) {
        if (x < width && y < height) {
            return list.get(y).get(x);
        }else {
            return -1;
        }
    }




    public void putPixel(int x, int y, int i){

        list.get(x).set(y,i);




    }


    public void print(){
        for (int i = 0; i < height; i++) {
            for (int j = 0; j < width; j++) {
                System.out.print(list.get(i).get(j)+" ");
            }
            System.out.println();
        }


    }

    public int getWidth() {
        return width;
    }

    public int getHeight() {

        return height;
    }
}
