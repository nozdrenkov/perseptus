#include <iostream>
#include <fstream>
#include <string>
#include <iomanip>
#include <vector>
#include <Windows.h>
#include <opencv2\opencv.hpp>

using namespace std;
using namespace cv;

typedef unsigned char uchar;
typedef unsigned long long phash;

void dbg(const string &name, Mat show) {
#if 0
    Mat img = show.clone();
    resize(img, img, Size(256, 256), 0, 0, INTER_AREA);
    imshow(name, img);
#endif
}

double comparePerHash(phash a, phash b) {
    int cnt = 0;
    for (int i = 0; i < 64; ++i) {
        int aa = (a >> i) & 1;
        int bb = (b >> i) & 1;
        cnt += aa == bb;
    }
    return 100 * double(cnt) / 64;
}

phash getPerceptualHash(Mat img) {
    const Size hash_size = Size(8, 8);

    resize(img, img, hash_size, 0, 0, INTER_AREA);
    dbg("small_image", img);
    cvtColor(img, img, CV_BGR2GRAY);
    dbg("small_gray", img);

    double avg = 0;
    for (int i = 0; i < hash_size.height; ++i) {
        for (int j = 0; j < hash_size.width; ++j) {
            avg += img.at<unsigned char>(i, j);
        }
    }
    avg /= hash_size.height * hash_size.width;

    phash res = 0;
    for (int i = 0; i < hash_size.height; ++i) {
        for (int j = 0; j < hash_size.width; ++j) {
            uchar &cur = img.at<uchar>(i, j);
            res |= phash(cur > avg) << (i * hash_size.width + j);
            cur = int(cur > avg) * 255;
        }
    }

    dbg("black_white", img);
    return res;
}

void compareImages(const string &apath, const string &bpath) {
    Mat a = imread(apath);
    Mat b = imread(bpath);

    phash ha = getPerceptualHash(a);
    phash hb = getPerceptualHash(b);

    cout << "hash A = " << hex << ha << endl;
    cout << "hash B = " << hb << endl;
    cout << "similar = " << fixed << setprecision(2) << dec << comparePerHash(ha, hb) << "%" << endl;

    resize(a, a, Size(), 0.2, 0.2);
    imshow("img a", a);
    resize(b, b, Size(), 0.2, 0.2);
    imshow("img b", b);

    waitKey(0);
}

int main(int argc, char **argv) {
    if (argc != 3) {
        cout << "usage:" << endl << "p-hash image1 image2" << endl;
        return 0;
    }
    compareImages(string(argv[1]), string(argv[2]));
    return 0;
}