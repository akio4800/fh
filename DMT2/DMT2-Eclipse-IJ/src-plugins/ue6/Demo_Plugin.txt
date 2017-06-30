package ue6;

import com.sun.prism.Image;

import ij.ImagePlus;
import ij.io.LogStream;
import ij.plugin.filter.PlugInFilter;
import ij.process.ByteProcessor;
import ij.process.ImageProcessor;

import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

/**
 * Example ImageJ Plugin that inverts an 8-bit grayscale image.
 */
public class Demo_Plugin implements PlugInFilter {

    public int setup(String args, ImagePlus image) {
        LogStream.redirectSystem();    // make System.out.println() work
        return DOES_8G;
    }

    public void run(ImageProcessor ip) {

        //   System.out.println("Hello Hagenberg!");        // testing only
        int w = ip.getWidth();
        int h = ip.getHeight();

        for (int y = 0; y < h; y++) {
            System.out.print("|" + y + ": ");

            int x = 0;
            while (x < w) {
                int x_end = findRun(ip, x, y);
                markRun(ip, x, x_end, y);
                System.out.print(x_end - x + " ");
                x = x_end;

            }
            System.out.println();
        }


        List<Integer> code = encode(ip);

        printCode(code);

        ImageProcessor ip2 = decode(code);

        new ImagePlus("Reconstructed Image", ip2).show();


        invert(ip, h, w);

        System.out.println("Size of img:" + (ip.getWidth() * ip.getHeight()) / 8.0);

        System.out.println("Size of code: " + code.size() * 4);

        System.out.println("Compression: " + ((ip.getWidth() * ip.getHeight()) / 8.0) / (code.size() * 4));

    }

    public int findRun(ImageProcessor ip, int x_start, int y) {

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

    List<Integer> encodeRun(int lengh) {
        List<Integer> list = new ArrayList<>();

        while (lengh > 254) {
            list.add(254);
            list.add(0);
            lengh -= 254;
        }
        list.add(lengh);
        return list;


    }


    List<Integer> encode(ImageProcessor ip) {

        List<Integer> list = new ArrayList<>();
        int w = ip.getWidth();
        int h = ip.getHeight();

        for (int y = 0; y < h; y++) {


            int x = 0;
            if (ip.getPixel(x, y) == 255) {
                list.add(0);
            }
            while (x < w) {
                int x_end = findRun(ip, x, y);
                // markRun(ip, x, x_end, y);
                list.addAll(encodeRun(x_end - x));
                x = x_end;

            }
            list.add(255);
        }


        return list;
    }


    ImageProcessor decode(List<Integer> code) {


        int height = Collections.frequency(code, 255);

        int width = 0;
        int i = 0;
        while (code.get(i) != 255) {
            width += code.get(i);
            i++;

        }

        System.out.println(height + " | " + width);


        ImageProcessor ip2 = new ByteProcessor(width, height);

        int pos = 0;
        int it = 0;
        int y = 0;

        boolean color = true;

        if (code.get(0) != it) {
            color = false;
            System.out.println("c");

        }


        for (int j = 0; j < code.size(); j++) {
            if (y == it) {


                color = !color;
            }

            if (code.get(j) != 255) {
                for (int k = 0; k < code.get(j); k++) {

                    if (color) {
                        ip2.putPixel(pos, y, 255);
                    } else {
                        ip2.putPixel(pos, y, 0);
                    }
                    pos++;

                }


            }


            if (code.get(j) == 255) {
                y++;
                pos = 0;
                color = true;


            }


            if (code.get(j) != 254) {
                color = !color;

            } else if (code.get(j) != 255) {

                color = !color;
            }

        }
        for (int j = 0; j < width; j++) {
            ip2.putPixel(j, 0, 255 - ip2.getPixel(j, 0));
        }


        return ip2;
    }


    void printCode(List<Integer> list) {

        for (int i : list) {
            System.out.print(i + " ");
        }
        System.out.println();
    }

    void markRun(ImageProcessor ip, int x_start, int x_end, int y) {

        for (int i = x_start; i < x_end; i++) {
            if (ip.getPixel(i, y) > 0) {

                ip.putPixel(i, y, 200);
            } else {
                ip.putPixel(i, y, 100);
            }

        }


    }

    void invert(ImageProcessor ip, int h, int w) {
        for (int x = 0; x < w; x++) {
            for (int y = 0; y < h; y++) {
                int val = ip.getPixel(x, y);    // get pixel value at (x,y)
                ip.putPixel(x, y, 255 - val);    // set pixel value at (x,y)
            }
        }
    }
}