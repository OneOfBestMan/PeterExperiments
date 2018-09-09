# 实验证明，用filter方法返回的可迭代对象的操作并不会影响原对象
# filter方法返回的是一个可迭代对象，如果把这个对象传给list(filter)
# 那么原来的filter对象将没有用了；如果把filter对象迭代了，相当于把迭代用了，
# 那么再把这个filter传给list,那么list也是个空对象，没有用

import threading
import time
class Item(object):
      def __init__(self,name,age,isVisited,isDownload):
          self.name=name
          self.age=age
          self.isVisited=isVisited
          self.isDownload=isDownload
class FilterListTest(object):
      globalList=[]
      def prn_obj(self,obj):
          print('_'.join(['%s:%s' % item for item in obj.__dict__.items()]))
      def filterTest(self):
          for num in range(1,100):
              item=Item("name"+str(num),num,False,False)
              FilterListTest.globalList.append(item)
              print("添加:"+item.name)
          filterdList=filter(lambda d:d.isVisited==False and d.age<20,FilterListTest.globalList)
          #listFilteredList=list(filterdList)
          for f in filterdList:
              f.isVisited=True
              f.isDownload=True
              print("filtered obj")
              self.prn_obj(f)
              print("original obj")
              self.prn_obj(FilterListTest.globalList[f.age-1])
          listFilteredList=list(filterdList)
          for l in listFilteredList:
              l.isVisited=False
              l.newPro="new"
              print("listfiltered obj")
              self.prn_obj(l)
              #print("filtered obj")
              #self.prn_obj(filterdList[l.age])
              print("original obj")
              self.prn_obj(FilterListTest.globalList[l.age-1])

t=FilterListTest()
t.filterTest()

          


