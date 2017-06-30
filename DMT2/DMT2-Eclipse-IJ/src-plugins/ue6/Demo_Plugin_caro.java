package ue6;

import java.util.ArrayList;
import java.util.List;

import compression.HuffmanCoder;
import ij.ImagePlus;
import ij.io.LogStream;
import ij.plugin.filter.PlugInFilter;
import ij.process.ByteProcessor;
import ij.process.ImageProcessor;

public class Demo_Plugin_caro implements PlugInFilter
{
	public int setup(String args, ImagePlus image)
	{
		LogStream.redirectSystem(); // make System.out.println() work
		return DOES_8G;
	}

	public void run(ImageProcessor ip)
	{
		System.out.println("new imageprocessor who dis"); // testing only
		int w = ip.getWidth();
		int h = ip.getHeight();

		// invertImage(ip, w, h);

		// Aufgabe 6.1 ------------------------------------------------------
		{
			for (int y = 0; y < h; y++) {
				System.out.print("|" + y + ":");
				int x = 0;
				while (x < w) {
					int x_end = findRun(ip, x, y);
					// markRun(ip, x, x_end, y);
					System.out.print(" " + (x_end - x));
					x = x_end;
				}
				System.out.println("");
			}
		}

		System.out.println("\n======================================================\n");

		// Aufgabe 6.2 ------------------------------------------------------
		{
			List<Integer> encoded = encode(ip);
			printCode(encoded);
		}

		System.out.println("\n======================================================\n");

		// Aufgabe 6.3 ------------------------------------------------------
		{
			new ImagePlus("Reconstructed image", decode(encode(ip))).show();
		}

		// Aufgabe 6.4
		{
			List<Integer> code = encode(ip);
			String c = "";
			for (Integer i : code) {
				c += i.toString();
			}
			char[] OIArray = c.toCharArray();
			String OI = new String(OIArray);
			double[] p = getCharProbabilities(OIArray);
			HuffmanCoder hc = new HuffmanCoder(p);
			String encoded = hc.encode(OI);
			System.out.println(hc.getAlphabet());
			System.out.println("Huffman-code: " + encoded);
			System.out.println("Length of the Huffman-code: " + encoded.length());
		}
	}

	public void invertImage(ImageProcessor ip, int w, int h)
	{
		for (int y = 0; y < h; y++) {
			for (int x = 0; x < w; x++) {
				int val = ip.getPixel(x, y); // get pixel value at (x,y)
				ip.putPixel(x, y, 255 - val); // set pixel value at (x,y)
			}
		}
	}

	public int findRun(ImageProcessor ip, int x_start, int y)
	{
		int val = ip.getPixel(x_start, y);
		int w = ip.getWidth();
		while (x_start < w) {
			int p = ip.getPixel(x_start, y);
			if (val == p) {
				x_start++;
			} else {
				return x_start;
			}
		}
		return x_start;
	}

	public void markRun(ImageProcessor ip, int x_start, int x_end, int y)
	{
		while (x_start < x_end) {
			if (ip.getPixel(x_start, y) > 0) {
				ip.putPixel(x_start, y, 200);
			} else {
				ip.putPixel(x_start, y, 100);
			}
			x_start++;
		}
	}

	public List<Integer> encodeRun(int length)
	{
		List<Integer> encodedRun = new ArrayList<>();
		while (length > 254) {
			encodedRun.add(254);
			encodedRun.add(0);
			length -= 254;
		}
		encodedRun.add(length);
		return encodedRun;
	}

	public List<Integer> encode(ImageProcessor ip)
	{
		List<Integer> encoded = new ArrayList<>();
		int h = ip.getHeight();
		int w = ip.getWidth();
		for (int y = 0; y < h; y++) {
			int x = 0;
			if (ip.getPixel(x, y) == 255) {
				encoded.add(0);
			}
			while (x < w) {
				int x_end = findRun(ip, x, y);
				encoded.addAll(encodeRun(x_end - x));
				x = x_end;
			}
			encoded.add(255);
		}
		return encoded;
	}

	public void printCode(List<Integer> code)
	{
		for (int i : code) {
			System.out.print(i + " ");
		}
		System.out.println();
		System.out.println("Length of the run-length code: " + getRLCLength(code) + " bytes");
	}

	public int getRLCLength(List<Integer> code)
	{
		String s = "";
		for (int i : code) {
			s += i;
		}
		return s.length();
	}

	public ImageProcessor decode(List<Integer> code)
	{
		int h = 0;
		int w = 0;

		// ist es wirklich nötig, zwei mal durch die Liste durchzugehen? Mir ist kein Weg
		// eingefallen, das in nur einer for-Schleife zu machen, außer dass ich da w über und über
		// berechnen würde... aber dieser Weg kommt mir auch so ineffizient vor >n>
		for (int i : code) {
		if (i == 255) {
			h++;
		}
	}
		for (int i : code) {
			if (i == 255) {
				break;
			}
			w += i;
		}

		ImageProcessor ip2 = new ByteProcessor(w, h);

		boolean white = false;
		int x = 0;
		int y = 0;
		int counter = 0;
		for (int i = 0; i < code.size(); i++) {
			if (i == 0 && code.get(i) == 0 || code.get(i) == 0 && code.get(i - 1) == 255) {
				white = true;
				continue;
			} else if (code.get(i) == 0) {
				white = !white;
				continue;
			}
			if (code.get(i) == 255) {
				x = 0;
				counter = 0;
				white = false;
				y++;
				continue;
			}
			int color = white ? 255 : 0;
			while (counter < code.get(i)) {
				ip2.putPixel(x++, y, color);
				if (x == w) {
					x = 0;
				}
				counter++;
			}
			counter = 0;
			white = !white;
		}
		return ip2;
	}

	private static double[] getCharProbabilities(char[] textArray)
	{
		double[] p = new double[256]; // contains the probability of each number from 0 - 255
		for (char zi : textArray) {
			p[zi]++;
		}
		for (int i = 0; i < p.length; i++) {
			p[i] = p[i] / textArray.length;
		}
		return p;
	}
}