import pickle
class Serialize(object):
    def unpickle(self,file):
        f = open(file,'rb')    
        bb = f.read()
        o = list(pickle.loads(bb))
        f.close()
        o=self.indeep(o)
        return o

    def indeep(self,var):
        x = 0;
        count = len(var)
        while x < count:
            if type(var[x])==tuple or type(var[x])==list:
                var[x]=self.indeep(list(var[x]))
                x += 1
            else:
                if type(var[x])==str:
                    try:
                        if x==27:
                            a=0
                        else:
                            o = list(pickle.loads(var[x]))
                            var[x]=self.indeep(o)
                    except:
                        pass
                x += 1
        return var