import numpy as np
import matplotlib.pyplot as plt
from scipy import optimize

class nihe(object):
      
      def __init__(self):
          print("begin")

      def show(self):
          x=np.arange(1,13,1)
          y=np.array([17, 19, 21, 28, 33, 38, 37, 37, 31, 23, 19, 18 ])
          plt.plot(x,y)
          plt.show()

      def fmax(self,x,a,b,c):
         return a*np.sin(x*np.pi/6+b)+c

      def qiuzhi(self):
         x=np.arange(1,13,1)
         x1=np.arange(1,13,0.1)
         ymax=np.array([17, 19, 21, 28, 33, 38, 37, 37, 31, 23, 19, 18 ])
         fita,fitb=optimize.curve_fit(self.fmax,x,ymax,[1,1,1])
         print(fita)
         plt.plot(x,ymax)
         plt.plot(x1,self.fmax(x1,fita[0],fita[1],fita[2]))
         plt.show()



test=nihe()
#test.show()
test.qiuzhi()


